﻿using System;
using System.Globalization;
using System.Web.Mvc;

namespace FreeMarket.Models
{
    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            ModelState modelState = new ModelState { Value = valueResult };

            object actualValue = null;

            if (valueResult.AttemptedValue.StartsWith("12345"))
            {
                int i = 0;
            }

            if (valueResult.AttemptedValue != string.Empty)
            {
                try
                {
                    if (valueResult.AttemptedValue.Contains("."))
                    {
                        actualValue = Convert.ToDecimal(valueResult.AttemptedValue, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        actualValue = Convert.ToDecimal(valueResult.AttemptedValue, CultureInfo.CurrentCulture);
                    }
                }
                catch (FormatException e)
                {
                    modelState.Errors.Add(e);
                }
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);

            return actualValue;
        }
    }
}