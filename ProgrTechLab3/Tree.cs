using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using ProgrTechLab3;

public class Tree : INotifyPropertyChanged
{
    public enum LeafTypes
    {
        Лиственное,
        Хвойное
    }

    private string _name;
    private LeafTypes _leafType;
    private int _numberOfTypes;
    private double _averageLifetime;
    private float _maxHeight;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    //public class EmptyNameException : Exception
    //{
    //    public EmptyNameException()
    //    { }
    //    public EmptyNameException(string message)
    //        : base(message)
    //    { }
    //    public EmptyNameException(string message, Exception innerException)
    //        : base(message, innerException)
    //    { }
    //}

    [DisplayName("Название")]

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }
    [DisplayName("Вид листьев")]
    public LeafTypes LeafType
    {
        get => _leafType;
        set
        {
            _leafType = value;
            OnPropertyChanged(nameof(LeafType));
        }
    }
    [JsonIgnore]
    [Browsable(false)]
    public int IntLeafType
    {
        get => (int)LeafType;
        set
        {
            LeafType = (LeafTypes)value;
        }
    }

    [DisplayName("Количество видов")]
    public int NumberOfTypes
    {
        get => _numberOfTypes;
        set
        {
            _numberOfTypes = value;
            OnPropertyChanged(nameof(NumberOfTypes));
        }
    }
    [DisplayName("Среднее время жизни")]
    public double AverageLifetime
    {
        get => _averageLifetime;
        set
        {
            _averageLifetime = value;
            OnPropertyChanged(nameof(AverageLifetime));
        }
    }
    [DisplayName("Максимальная высота")]
    public float MaxHeight
    {
        get => _maxHeight;
        set
        {
            _maxHeight = value;
            OnPropertyChanged(nameof(MaxHeight));
        }
    }

 
    //public int MaxHeightInt
    //{
    //    get
    //    {
    //        return (int)(MaxHeight * 10);
    //    }
        
    //}

    public Tree(string name, LeafTypes leafType, int numberOfTypes, double averageLifeTIme, float maxHeight)
    {
        Name = name;
        LeafType = leafType;
        NumberOfTypes = numberOfTypes;
        AverageLifetime = averageLifeTIme;
        MaxHeight = maxHeight;
    }

    public Tree CloneTree() => new(Name, LeafType, NumberOfTypes, AverageLifetime, MaxHeight)
    {
        Name = this.Name,
        LeafType = this.LeafType,
        NumberOfTypes = this.NumberOfTypes,
        AverageLifetime = this.AverageLifetime,
        MaxHeight = this.MaxHeight
    };
    //public Tree CloneTree()
    //{
    //    Tree cloneTree = new Tree(this.Name, this.LeafType, this.NumberOfTypes, this.AverageLifetime, this.MaxHeight);
    //    return cloneTree;
    //}

}
