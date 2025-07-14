using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProyectoPrograCliente.Models
{
    public class Monster
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string? MonsterName { get; set; }
        public string? MonsterType1 { get; set; }
        public string? MonsterType2 { get; set; }
        public int MonsterAttack { get; set; }
        public int MonsterSpecialAttack { get; set; }
        public int MonsterDefense { get; set; }
        public int MonsterSpecialDefense { get; set; }
        public int MonsterSpeed { get; set; }
        public int MonsterHealth { get; set; }
        public int MonsterCurrentHealth { get; set; }
        public string? Sprite { get; set; }
        private const string BaseUrl = "https://localhost:7117/";
        [Ignore]
        public string SpriteFullUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(MonsterName))
                    return "placeholder.png";

                string name = MonsterName.ToLowerInvariant().Trim().Replace(" ", "");
                return $"{name}_1.png";
            }
        }

    }
}