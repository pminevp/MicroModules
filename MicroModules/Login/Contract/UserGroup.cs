﻿
using System;

namespace MicroModules.Login.Contract
{
  public  class UserGroup : IUserGroup
    {
        public int GroupId { get; set; }
        public string AccessFormName {get;  set; }

    }
}
