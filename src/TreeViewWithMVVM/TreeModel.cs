using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TreeViewWithMVVM
{
    public abstract class TreeModel<T> : ObservableObject
    {
        #region ctor
        public TreeModel()
        {
            IsSelected = false;
            children = new ObservableCollection<TreeModel<T>>();
        }
        #endregion

        #region properties
        private TreeModel<T> parent;
        public TreeModel<T> Parent
        {
            get => parent;
            set => SetProperty(ref parent, value);
        }

        protected ObservableCollection<TreeModel<T>> children;
        public IEnumerable<TreeModel<T>> Children => children;

        private T selectedValue;
        public T SelectedValue
        {
            get => selectedValue;
            set => SetProperty(ref selectedValue, value);
        }

        private string displayText;
        public string DisplayText
        {
            get => displayText;
            set => SetProperty(ref displayText, value);
        }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get => isExpanded;
            set => SetProperty(ref isExpanded, value);
        }
        #endregion

        #region methods
        public override string ToString()
        {
            return DisplayText;
        }

        public void AddChild(TreeModel<T> child)
        {
            child.Parent = this;
            children.Add(child);
        }
        #endregion

        #region static methods
        public static TreeModel<T> GetNodeById(T id, IEnumerable<TreeModel<T>> nodes)
        {
            foreach (var node in nodes)
            {
                if (node.SelectedValue.Equals(id)) return node;

                TreeModel<T> foundChild = GetNodeById(id, node.Children);
                if (foundChild != null) return foundChild;
            }
            return null;
        }

        public static TreeModel<T> GetSelectedNode(IEnumerable<TreeModel<T>> nodes)
        {
            foreach (var node in nodes)
            {
                if (node.IsSelected) return node;

                TreeModel<T> selectedChild = GetSelectedNode(node.Children);
                if (selectedChild != null) return selectedChild;
            }
            return null;
        }

        public static void ExpandParentNodes(TreeModel<T> node)
        {
            if (node.Parent != null)
            {
                node.Parent.IsExpanded = true;
                ExpandParentNodes(node.Parent);
            }
        }

        public static void ToggleExpanded(IEnumerable<TreeModel<T>> nodes, bool isExpanded)
        {
            foreach (var node in nodes)
            {
                node.IsExpanded = isExpanded;
                ToggleExpanded(node.Children, isExpanded);
            }
        }
        #endregion
    }

    public class TreeModel : TreeModel<Guid>
    {
    }
}