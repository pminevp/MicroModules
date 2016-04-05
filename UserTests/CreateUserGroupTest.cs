﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroModules.Login.Managers;
using MicroModules.Login.Contract;

namespace UserTests
{
    [TestClass]
    public class CreateUserGroupTest
    {
        string constring = "Data Source=.;Initial Catalog=test;Integrated Security=True";

        [TestMethod]
        public void NewGroup()
        {
            var groupM = new UserGroupManager(constring);
            groupM.CreateGroup("Administrator");
        }


        [TestMethod]
        public void NewUser()
        {
            var usrGroup = new UserGroup { Name= "Administrator", GroupId=1 };

            IUser usr = new User { UserName="pm", password="123", Fammily="aa", FirstName="ee", permissions= new System.Collections.Generic.List<IUserGroup> { usrGroup } };

            var usrm = new UserManager(constring);
            usr= usrm.Create(usr);
            usrm.AddPermission(usr);

        }

        [TestMethod]
        public void GetUserGroups()
        {
            var usrm = new UserGroupManager(constring);
            var allgroups=usrm.GetGroups();
        }

        [TestMethod]
        public void GetUserGroupsByUserId()
        {
            var usrm = new UserGroupManager(constring);
            var allgroups = usrm.GetGroups(1);
        }

        [TestMethod]
        public void LoginTest()
        {
            var usrm = new UserManager(constring);

            var user = usrm.Login("pm", "123");
        }
    }
}