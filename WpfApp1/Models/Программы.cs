using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Программы
{
    public int Id { get; set; }

    public string Название { get; set; } = null!;

    public int ВозрастМин { get; set; }

    public int ВозрастМакс { get; set; }

    public int FkУчреждение { get; set; }

    public virtual Учреждение FkУчреждениеNavigation { get; set; } = null!;

    public virtual ICollection<Смены> Сменыs { get; set; } = new List<Смены>();
}
