using Four.Presentation.Models;

namespace Four.Presentation.ViewModels; 

public class BaseViewModel<T> : BindableBase 
    where T: BaseModel<T>, new() 
{
    private T? _baseModel;
    private T? _editedModel;


    public BaseViewModel() {
        _baseModel = null;
        _editedModel = null;
    }


    public bool HasPendingChanges() {
        if (_baseModel == null) {
            return false;
        }

        return !_baseModel.Equals(_editedModel);
    }
}
