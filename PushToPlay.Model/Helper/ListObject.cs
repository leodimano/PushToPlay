using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model
{
    public class ListObject<T>
    {
        int _pageSize;
        int _totalObject;
        int _minRowNum;
        int _maxRowNum;

        public ListObject(int currentPage_ = 1)
        {
            _pageSize = PushToPlay.Helpers.NetHelper.GetPageSize();
            TotalObject = 0;
            TotalPages = 0;

            if (currentPage_ > 1)
            {
                CurrentPage = currentPage_;                
                _minRowNum = (_pageSize * (currentPage_ - 1)) + 1;
            }
            else
            {
                CurrentPage = 1;
                _minRowNum = 1;
            }

            _maxRowNum = _minRowNum + _pageSize - 1;
        }

        public List<T> Objects { get; set; }

        public int TotalObject
        {
            get
            {
                return _totalObject;
            }
            set
            {
                _totalObject = value;

                if (_totalObject > 0)
                {
                    TotalPages = Convert.ToInt32(Math.Ceiling((float)_totalObject / (float)_pageSize));
                }
            }
        }

        public int TotalPages { get; set; }

        public int GetMinRowNum
        {
            get
            {
                return _minRowNum;
            }
        }

        public int GetMaxRowNum
        {
            get
            {
                return _maxRowNum;
            }
        }

        public int CurrentPage { get; set; }
    }
}
