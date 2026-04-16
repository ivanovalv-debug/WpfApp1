using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Бронирование
{
    public int Id { get; set; }

    public int FkРебенка { get; set; }

    public int FkСмена { get; set; }

    public int FkОператор { get; set; }

    public string Статус { get; set; } = null!;

    public DateOnly ДатаОформления { get; set; }

    public virtual Люди FkОператорNavigation { get; set; } = null!;

    public virtual Ребенок FkРебенкаNavigation { get; set; } = null!;

    public virtual Смены FkСменаNavigation { get; set; } = null!;

    public virtual ICollection<ДневникСмены> ДневникСменыs { get; set; } = new List<ДневникСмены>();

    public virtual ICollection<Документы> Документыs { get; set; } = new List<Документы>();
}
