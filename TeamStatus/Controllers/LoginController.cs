using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using EntityObject.Utility;
using TeamStatus.JsonData;
using TeamStatus.Models;

namespace TeamStatus.Controllers
{
    public class LoginController : ApiController
    {
        public List<JsonUserEntity> Get()
        {
            var manager = new UserManger();;

            var data = manager.GetAllUserList();
            if (data == null) return null;
            if (data.Tables[0].Rows.Count == 0) return null;

            var jsonLoginEntity = new List<JsonUserEntity>();
            foreach (DataRow dataRow in data.Tables["Login"].Rows)
            {
                jsonLoginEntity.Add(new JsonUserEntity()
                {
                    UserId = ConvertValue.ToStringValue(dataRow["USERID"]),
                    UserName = ConvertValue.ToStringValue(dataRow["USERNAME"]),
                    Password = ConvertValue.ToStringValue(dataRow["PASSWORD"]),
                    Role = ConvertValue.ToStringValue(dataRow["ROLE"]),
                    Name = ConvertValue.ToStringValue(dataRow["NAME"])
                });
            }
            return jsonLoginEntity;
        }

        public string Get(string userName, string password)
        {
            return "value";
        }

        public object Post(UserEntity userEntity)
        {
            if (userEntity == null) return new HttpError("Entity is null");

            var manager = new UserManger();
            manager.Save(userEntity);

            return userEntity;
        }

        public object Put(UserEntity userEntity)
        {
            if (userEntity == null) return new HttpError("Entity is null");

            var manager = new UserManger();
            manager.Update(userEntity);

            return userEntity;
        }

        public long Delete(long id)
        {
            var manager = new UserManger();
            manager.Delete(id);
           
            return id;
        }
    }
}
