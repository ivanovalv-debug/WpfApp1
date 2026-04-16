using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Учреждение
{
    public int Id { get; set; }

    public string Название { get; set; } = null!;

    public string Тип { get; set; } = null!;

    public string Регион { get; set; } = null!;

    public string? Адрес { get; set; }

    public decimal? Рейтинг { get; set; }

    public virtual ICollection<Программы> Программыs { get; set; } = new List<Программы>();
}
