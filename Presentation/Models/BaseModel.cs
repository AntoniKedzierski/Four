namespace Four.Presentation.Models; 

public abstract class BaseModel<T> : BindableBase, IEquatable<T> 
    where T : class, new() 
{
    public abstract bool Equals(T? other);
}
