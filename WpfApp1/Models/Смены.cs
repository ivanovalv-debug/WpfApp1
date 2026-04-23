using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Смены
{
    public int Id { get; set; }

    public DateTime ДатаНачала { get; set; }

    public DateTime ДатаОкончания { get; set; }

    public decimal Стоимость { get; set; }

    public int Вместимость { get; set; }

    public int FkПрограмма { get; set; }

    public virtual Программы FkПрограммаNavigation { get; set; } = null!;

    public virtual ICollection<Бронирование> Бронированиеs { get; set; } = new List<Бронирование>();
}
