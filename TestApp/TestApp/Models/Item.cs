using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestApp.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string SetReminder { get; set; }
        public DateTime ReminderTime { get; set; }
    }
}