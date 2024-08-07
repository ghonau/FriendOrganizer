﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace FriendOrganizer.UI.Wrapper
{
    public class ModelWrapper<T> : NotifyDataErrorInfoBase
    {
        public ModelWrapper(T model)
        {
           Model  = model;
        }

        public T Model { get; set; }


        protected virtual TValue GetValue<TValue>([CallerMemberName] string? propertyName = null)
        {
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model); 
        }
        protected virtual void SetValue<TValue>(TValue value,[CallerMemberName] string? propertyName = null)
        {
            typeof(T).GetProperty(propertyName).SetValue(Model, value);
            OnPropertyChanged(propertyName);
            ValidatePropertyInternal(propertyName, value);
        }

        private void ValidatePropertyInternal(string propertyName, object currentValue)
        {
            ClearErrors(propertyName);

            // 1. Validate Data Annotation
            // 2. Validate Custom Errors 


            ValidateDataAnnotation(propertyName, currentValue);

            ValidateCustomErrors(propertyName);

        }

        private void ValidateDataAnnotation(string propertyName, object currentValue)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(Model)
            {
                MemberName = propertyName
            };

            Validator.TryValidateProperty(currentValue, context, results);
            foreach (var item in results)
            {
                AddError(propertyName, item.ErrorMessage);
            }
        }

        private void ValidateCustomErrors(string propertyName)
        {
            var errors = ValidateProperty(propertyName);
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    AddError(propertyName, error);
                }
            }
        }

        protected virtual IEnumerable<string> ValidateProperty(string propertyName) 
        {
            return null; 
        }
    }
}
