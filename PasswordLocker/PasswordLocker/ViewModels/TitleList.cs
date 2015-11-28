using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordLocker.ViewModels
{
    public class TitleList
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public void Set_TitleList(int id,string title)
        {
            this.Title=title;
            this.Id = id;
        }
    }
}
