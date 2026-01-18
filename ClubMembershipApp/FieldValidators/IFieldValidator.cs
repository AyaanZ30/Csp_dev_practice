using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubMembershipApp.FieldValidators
{
    public delegate bool FieldValidatorDel(int fieldIndex, string fieldVal, string[] FieldArray, out string fieldInvalidMessage);

    // interface just tells what MUST exist (blueprint)
    public interface IFieldValidator    // by default, everything is public in an interface => hence implementing class must match that visibility.
    {
        // method signature
        void initializeValidatorDelegates();

        // get : read-only access (no set => does not have to be writable)
        string[] FieldArray { get; }
        FieldValidatorDel ValidatorDel { get; }
    }
}