using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class ДневникСмены
{
    public int Id { get; set; }

    public string? Описание { get; set; }

    public DateTime ДатаСоздания { get; set; }

    public int FkБронирование { get; set; }

    public virtual Бронирование FkБронированиеNavigation { get; set; } = null!;
}
