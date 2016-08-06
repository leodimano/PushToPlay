using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using PushToPlay.Model;

namespace PushToPlay.WEB.Models
{
    public class GroupSearchViewModel : PushToPlayBaseViewModel
    {
        [Display(Name = "Buscar por...")]
        public string GroupName { get; set; }

        public string ResultText { get; set; }

        public List<GroupCompactViewModel> ListGroupCompactViewModel { get; set; }

        public void SetGroupList(List<Group> grouplList_)
        {
            this.ListGroupCompactViewModel = new List<GroupCompactViewModel>();

            if (grouplList_ != null)
            {
                foreach (var _detail in grouplList_)
                {
                    this.ListGroupCompactViewModel.Add(new GroupCompactViewModel(_detail));
                }
            }
        }
    }
}