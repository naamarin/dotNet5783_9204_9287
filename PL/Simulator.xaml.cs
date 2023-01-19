using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;

namespace PL
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Window
    {
        DateTime dateTime = DateTime.Now;
        BackgroundWorker timerworker;
        static readonly Random rand = new Random();

        BlApi.IBl? bl = BlApi.Factory.Get();

        public ObservableCollection<BO.OrderForList?> currentOrder
        {
            get { return (ObservableCollection<BO.OrderForList?>)GetValue(currentcurrentOrder); }
            set { SetValue(currentcurrentOrder, value); }
        }

        public static readonly DependencyProperty currentcurrentOrder =
            DependencyProperty.Register("currentOrder", typeof(ObservableCollection<BO.OrderForList?>), typeof(Window), new PropertyMetadata(null));
        public Simulator()
        {
            InitializeComponent();
            currentOrder = new ObservableCollection<BO.OrderForList?>(bl!.Order.OrderListForManager());
            
            timerworker = new BackgroundWorker();
            timerworker.DoWork += Timerworker_DoWork;
            timerworker.ProgressChanged += Timerworker_ProgressChanged; 
            timerworker.RunWorkerCompleted += Timerworker_RunWorkerCompleted;

            timerworker.WorkerReportsProgress = true;
            timerworker.WorkerSupportsCancellation = true;

        }

        private void Timerworker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                MessageBox.Show("Try next time...");
            }
            else
            {
                MessageBox.Show("finished!");
            }
        }

        private void Timerworker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            BO.Order order = null;
            TimeSpan diffrence;
            foreach (var item in currentOrder)
            {
                order = bl.Order.GetById(item!.ID);

                diffrence = dateTime - order.OrderDate.Value;
                if (item!.Status == BO.OrderStatus.Ordered && diffrence.TotalMinutes > 30)
                    bl!.Order.OrderShipUpdate(item.ID);
                else if (order.ShipDate != null)
                {
                    diffrence = dateTime - order.ShipDate.Value;
                    if (item!.Status == BO.OrderStatus.Shipped && diffrence.TotalMinutes > 30)
                        bl!.Order.OrderDeliveryUpdate(item.ID);
                }
                
            }
            currentOrder = new ObservableCollection<BO.OrderForList?>(bl!.Order.OrderListForManager());
        }

        private void Timerworker_DoWork(object? sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 30; i++)
            {
                if (timerworker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    Thread.Sleep(1000);

                    timerworker.ReportProgress(i/30*100);
                    if (timerworker.WorkerReportsProgress == true)
                        dateTime = dateTime.AddMinutes(2);
                }
            }
        }

        private void btStartTracking_Click(object sender, RoutedEventArgs e)
        {
            if (timerworker.IsBusy != true)
            {
                this.Cursor = Cursors.Wait;
                timerworker.RunWorkerAsync();
            }
        }

        private void btStopTracking_Click(object sender, RoutedEventArgs e)
        {
            if (timerworker.WorkerSupportsCancellation == true)
                timerworker.CancelAsync(); 
        }

        private void btViewOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var orderForList = (BO.OrderForList)button.DataContext;
            new OrderTrackingView(orderForList.ID).ShowDialog();
        }
    }
}
