import { ValidatorFn, FormGroup, ValidationErrors } from "@angular/forms";

export const passwordUnConfirmedValidator: ValidatorFn = 
// lambda
(control: FormGroup): ValidationErrors | null => {
    const password = control.get('password');
    const repeatedPassword = control.get('repeatedPassword');
    let result = password.value === repeatedPassword.value ? null : { 'passwordUnconfirmed' : true };

    if (result != null) {
        console.log(`passwordUnconfirmed: ${ result.passwordUnconfirmed } => (${password.value} : ${repeatedPassword.value})`);
    }
    
    return result;
};