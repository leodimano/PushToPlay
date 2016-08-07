using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PushToPlay.Model
{
    public enum RelationFriendEnum
    {
        Undefined = -1,

        NotFriend = 0,

        Asked = 1,
        Pending = 2,

        Friend = 3,
    }

    public partial class RelationFriend : IDisposable
    {
        public void Dispose()
        {
        }

        public static void RequestFriend(int userBase_, int userTarget_)
        {
            var _relationFriendBase = GetRelation(userBase_, userTarget_);
            var _relationFriendTarget = GetRelation(userTarget_, userBase_);

            if (_relationFriendBase == null)
            {
                _relationFriendBase = new RelationFriend();
                _relationFriendBase.Status = (int)RelationFriendEnum.Asked;
                _relationFriendBase.UserBaseId = userBase_;
                _relationFriendBase.UserTargetId = userTarget_;
                _relationFriendBase.Add();
            }

            if (_relationFriendTarget == null)
            {
                _relationFriendTarget = new RelationFriend();
                _relationFriendTarget.Status = (int)RelationFriendEnum.Pending;
                _relationFriendTarget.UserBaseId = userTarget_;
                _relationFriendTarget.UserTargetId = userBase_;
                _relationFriendTarget.Add();
            }
        }

        public static void AcceptRequest(int userBase_, int userTarget_)
        {
            var _relationFriendBase = GetRelation(userBase_, userTarget_);
            var _relationFriendTarget = GetRelation(userTarget_, userBase_);

            if (_relationFriendBase != null)
            {
                _relationFriendBase.Status = (int)RelationFriendEnum.Friend;
                _relationFriendBase.Update();
            }

            if (_relationFriendTarget != null)
            {
                _relationFriendTarget.Status = (int)RelationFriendEnum.Friend;
                _relationFriendTarget.Update();
            }
        }

        public static void DenyRequest(int userBase_, int userTarget_)
        {
            DeleteFriend(userBase_, userTarget_);
        }

        public static void DeleteFriend(int userBase_, int userTarget_)
        {
            var _relationFriendBase = GetRelation(userBase_, userTarget_);
            var _relationFriendTarget = GetRelation(userTarget_, userBase_);

            if (_relationFriendBase != null)
                _relationFriendBase.Delete();

            if (_relationFriendTarget != null)
                _relationFriendTarget.Delete();
        }

        public static RelationFriend GetRelation(int userBase_, int userTarget_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = _context.RelationFriends.Where(rf => rf.UserBaseId == userBase_ && rf.UserTargetId == userTarget_);

                if (_query != null && _query.Count() > 0)
                    return _query.First();
                else
                    return null;
            }
        }

        public static int GetRelationCount(int userBase_, RelationFriendEnum status_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                return _context.RelationFriends.Count(rf => rf.UserBaseId == userBase_ && rf.Status == (int)status_);
            }
        }

        public static void RelationAction(int userBase_, int userTarget_, bool deny)
        {
            /* Check the relation */
            var _relation = GetRelation(userBase_, userTarget_);

            if (_relation != null)
            {
                switch ((RelationFriendEnum)_relation.Status)
                {
                    case RelationFriendEnum.NotFriend:
                        Model.RelationFriend.RequestFriend(userBase_, userTarget_);
                        break;
                    case RelationFriendEnum.Friend:
                        Model.RelationFriend.DeleteFriend(userBase_, userTarget_);
                        break;
                    case RelationFriendEnum.Asked:
                        Model.RelationFriend.DeleteFriend(userBase_, userTarget_);
                        break;
                    case RelationFriendEnum.Pending:
                        if (deny)
                        {
                            Model.RelationFriend.DenyRequest(userBase_, userTarget_);
                        }
                        else
                        {
                            Model.RelationFriend.AcceptRequest(userBase_, userTarget_);

                            Model.ActivityMessage.CreateActivity(userBase_,
                                                                 MessageSourceTypeEnum.User,
                                                                 userTarget_,
                                                                 MessageSourceTypeEnum.User,
                                                                 string.Empty,
                                                                 MessageTypeEnum.Activity,
                                                                 MessageActionEnum.FriendAdded,
                                                                 0);

                            Model.ActivityMessage.CreateActivity(userTarget_,
                                                                 MessageSourceTypeEnum.User,
                                                                 userBase_,
                                                                 MessageSourceTypeEnum.User,
                                                                 string.Empty,
                                                                 MessageTypeEnum.Activity,
                                                                 MessageActionEnum.FriendAccepted,
                                                                 0);
                        }
                        break;
                }
            }
            else
            {
                Model.RelationFriend.RequestFriend(userBase_, userTarget_);
            }
        }

        private void Add()
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                this.ModifiedDate = null;
                this.CreatedDate = DateTime.Now;

                _context.RelationFriends.Add(this);
                _context.SaveChanges();
            }
        }

        private void Update()
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                this.ModifiedDate = DateTime.Now;

                _context.Entry(this).State = System.Data.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        private void Delete()
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                _context.Entry(this).State = System.Data.EntityState.Deleted;
                _context.SaveChanges();
            }
        }

    }
}
