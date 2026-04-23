using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Ребенок
{
    public int Id { get; set; }

    public string Фио { get; set; } = null!;

    public DateTime ДатаРождения { get; set; }

    public string? МедДокументы { get; set; }

    public int FkРодитель { get; set; }

    public virtual Люди FkРодительNavigation { get; set; } = null!;

    public virtual ICollection<Бронирование> Бронированиеs { get; set; } = new List<Бронирование>();
}
