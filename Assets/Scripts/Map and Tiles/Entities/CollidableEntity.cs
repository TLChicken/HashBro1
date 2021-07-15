using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableEntity : Entity {

    public bool solidToObjs;
    //Contains ref to this entity's mover
    //If got no mover then HB cannot enter if this thing is solid
    //If got mover then onHBWantToEnter will ask the mover by calling the same fn there 
    public MovableEntityMover moverComponent;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
