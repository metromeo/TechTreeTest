using System.Collections;
using System.Collections.Generic;

public enum TechnologyID {
    None        = 0,
    Tech1       = 1,
    Tech2       = 2,
    Tech3       = 3,
    Tech4       = 4,
    Tech5       = 5,
    Tech6       = 6,
    Tech7       = 7,
    Tech8       = 8,
    Tech9       = 9,
    Tech10      = 10,
    Tech11      = 11,
    Tech12      = 12,
    Tech13      = 13,
    Count       
}

public enum TechnologyStatus {
    Disabled, Enabled, Completed
}

[System.Serializable]
public class Technology {
    public TechnologyID ID { get; }
    public TechnologyStatus Status { get; private set; }
    public string Description { get; }

    public Technology(Technology t) {
        ID = t.ID;
        Status = t.Status;
        Description = t.Description;
    }
    public Technology(TechnologyID id, TechnologyStatus status, string description) {
        ID = id;
        Status = status;
        Description = description;
    }

    public void SetComplete() {
        Status = TechnologyStatus.Completed;
    }
    public void SetEnabled() {
        Status = TechnologyStatus.Enabled;
    }
}
