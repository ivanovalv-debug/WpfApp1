using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Документы
{
    public int Id { get; set; }

    public string Тип { get; set; } = null!;

    public string? СсылкаНаФайлы { get; set; }

    public string? Статус { get; set; }

    public int FkБронирование { get; set; }

    public virtual Бронирование FkБронированиеNavigation { get; set; } = null!;
}
