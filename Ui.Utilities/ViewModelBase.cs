﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ui.Utilities
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T property, T newValue, [CallerMemberName] string memberName = null)
        {
            var old = property;
            property = newValue;
            if (!old.Equals(property))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
                return true;
            }

            return false;
        }
    }
}
