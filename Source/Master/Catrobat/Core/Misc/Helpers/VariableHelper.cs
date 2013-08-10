﻿using System.Collections.ObjectModel;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Variables;

namespace Catrobat.Core.Misc.Helpers
{
    public class VariableHelper
    {
        public static ObservableCollection<UserVariable> GetGlobalVariableList(Project project)
        {
            return project.VariableList.ProgramVariableList.UserVariables;
        }

        public static ObservableCollection<UserVariable> GetLocalVariableList(Project project, Sprite sprite)
        {
            foreach (var entry in project.VariableList.ObjectVariableList.ObjectVariableEntries)
            {
                if (entry.Sprite == sprite)
                    return entry.VariableList.UserVariables;
            }


            var newEntry = new ObjectVariableEntry
                {
                    Sprite = sprite,
                    VariableList = new UserVariableList()
                };
            project.VariableList.ObjectVariableList.ObjectVariableEntries.Add(newEntry);
            return newEntry.VariableList.UserVariables;
        }

        public static void DeleteGlobalVariable(Project project, UserVariable variable)
        {
            project.VariableList.ProgramVariableList.UserVariables.Remove(variable);
        }

        public static void DeleteLocalVariable(Project project, Sprite sprite, UserVariable variable)
        {
            foreach (var entry in project.VariableList.ObjectVariableList.ObjectVariableEntries)
            {
                if(entry.Sprite == sprite)
                    entry.VariableList.UserVariables.Remove(variable);
            }
        }

        public static void AddGlobalVariable(Project project, UserVariable variable)
        {
            project.VariableList.ProgramVariableList.UserVariables.Add(variable);
        }

        public static void AddLocalVariable(Project project, Sprite sprite, UserVariable variable)
        {
            foreach (var entry in project.VariableList.ObjectVariableList.ObjectVariableEntries)
            {
                if (entry.Sprite == sprite)
                {
                    entry.VariableList.UserVariables.Add(variable);
                    return;
                }
            }
            project.VariableList.ObjectVariableList.ObjectVariableEntries.Add(new ObjectVariableEntry
            {
                Sprite = sprite,
                VariableList = new UserVariableList
                {
                    UserVariables = new ObservableCollection<UserVariable> { variable } 
                }
            });
        }

        public static bool IsVariableLocal(Project project, UserVariable variable)
        {
            return !project.VariableList.ProgramVariableList.UserVariables.Contains(variable);
        }

        public static UserVariable CreateUniqueLocalVariable(Project project, Sprite sprite)
        {
            // TODO: implement me
            return new UserVariable { Name = sprite.Name + "_a"};
        }

        public static UserVariable CreateUniqueGlobalVariable(Project project)
        {
            // TODO: implement me
            return new UserVariable { Name = "global_a" };
        }
    }
}