using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Люди
{
    public int Id { get; set; }

    public string Фио { get; set; } = null!;

    public string Логин { get; set; } = null!;

    public string Пароль { get; set; } = null!;

    public string Роль { get; set; } = null!;

    public string? Телефон { get; set; }

    public string? Почта { get; set; }

    public string? Паспорт { get; set; }

    public virtual ICollection<Бронирование> Бронированиеs { get; set; } = new List<Бронирование>();

    public virtual ICollection<Ребенок> Ребенокs { get; set; } = new List<Ребенок>();
}
