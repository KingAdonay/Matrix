﻿using DataService.Context;
using DataService.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataService.Entities.Pedagogy;

namespace DataService.DataManager
{
    /// <summary>
    /// Manager des Classes
    /// </summary>
    public class ClassesManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myClasse"></param>
        /// <returns></returns>
        public bool AddClasse(Classe myClasse)
        {
            using (var db = new Ef())
            {
                db.Classe.Add(myClasse);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myClasse"></param>
        /// <returns></returns>
        public bool UpdateClasse(Classe myClasse)
        {
            using (var db = new Ef())
            {
                db.Classe.Attach(myClasse);
                db.Entry(myClasse).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classeId"></param>
        /// <returns></returns>
        public bool DeleteClasse(Guid classeId)
        {
            using (var db = new Ef())
            {
                db.Classe.Remove(db.Classe.Find(classeId));
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classeId"></param>
        /// <returns></returns>
        public Classe GetClasseById(Guid classeId)
        {
            using (var db = new Ef())
            {
                return db.Classe.Find(classeId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classeName"></param>
        /// <returns></returns>
        public static Classe GetClasseByName(string classeName)
        {
            using (var db = new Ef())
            {
                var myClasse = db.Classe.SingleOrDefault(s => s.Name == classeName);

                return myClasse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Classe> GetAllClasse()
        {
            using (var db = new Ef())
            {
                return db.Classe.ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classeId"></param>
        /// <returns></returns>
        public string GetClasseName(Guid classeId)
        {
            using (var db = new Ef())
            {
                return db.Classe.Find(classeId).Name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classeId"></param>
        /// <returns></returns>
        public bool ClasseExist(string classeId)
        {
            using (var db = new Ef())
            {
                return db.Classe.Find(classeId) != null;
            }
        }

        /// <summary>
        /// Renvoi les matieres Enregistrees pour cette classe
        /// </summary>
        /// <param name="classeId"></param>
        /// <returns></returns>
        public List<Matiere> GetClassMatieres(Guid classeId)
        {
            using (var db = new Ef())
            {
                return db.Matiere.Where(m => m.ClasseId == classeId).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public List<Staff> GetClassStaffs(Guid classId)
        {
            var staffs = new List<Staff>();

            using (var db = new Ef())
            {
                foreach (var st in db.Cours.Where(c => c.ClasseId == classId))
                {
                    staffs.Add(db.Staff.Find(st.StaffId));
                }
                return staffs;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public List<Student> GetClassStudents(Guid classId)
        {
            var students = new List<Student>();

            using (var db = new Ef())
            {
                foreach (var st in db.Inscription.Where(c => c.ClasseId == classId))
                {
                    students.Add(db.Student.Find(st.StudentId));
                }
                return students;
            }
        }

    }
}