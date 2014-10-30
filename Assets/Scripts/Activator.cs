using UnityEngine;
using System.Collections;

public class Activator : MonoBehaviour {

	public string type = "none";
	public virtual void Activate() { }
    public virtual void SetAsInactive() {  }
    public virtual void SetAsPlayerSwitch() { }
    public virtual void SetAsVineSwitch() { }
}
