using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViscoveryDemo.BLL.Models
{
    public enum OrderStatus
    {
        Waiting,
        Confirm,
        Undetected,
        UnMatch
    }

    public class Order : INotifyPropertyChanged
    {
        private OrderStatus _status = OrderStatus.Waiting;

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public OrderStatus Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
