﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// category of product (BO)
/// </summary>
public enum Category { Meals, Children, Extras, Desserts, Drinks, Sauces };
/// <summary>
/// status of order (BO)
/// </summary>
public enum OrderStatus { Ordered, Shipped, Delivered};