import { NgModule } from '@angular/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatInputModule }  from '@angular/material/input';

@NgModule({
    imports: [MatAutocompleteModule,MatInputModule],
    exports: [MatAutocompleteModule,MatInputModule]
})

export class AngularMaterialModule { }