import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";
import {StudentGetAllResponse} from "./StudentGetAllResponse";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-studenti',
  templateUrl: './studenti.component.html',
  styleUrls: ['./studenti.component.css']
})
export class StudentiComponent implements OnInit {

  title:string = 'angularFIT2';
  ime_prezime:string = '';
  opstina: string = '';
  studentPodaci: StudentGetAllResponse[]=[];
  filter_ime_prezime: boolean;
  filter_opstina: boolean;
  novi_student:any
  id:number
  ime:string
  prezime:string
  opstina_rodjenja_id:number
  opstine:any
  edit_student: any;
  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

  testirajWebApi() :void
  {
    this.httpKlijent.get<StudentGetAllResponse[]>(MojConfig.adresa_servera+ "/Student/GetAll", MojConfig.http_opcije()).subscribe(x=>{
      this.studentPodaci = x;
    });
  }

  ucitajDefaultneOpstine(){
    this.httpKlijent.put(MojConfig.adresa_servera+'/DodajDeafultOpstine', MojConfig.http_opcije()).subscribe(()=>{
      console.log("Uspjesno dodane defaultne opstine za sve korisnicke naloge! :D");
    })
  }

  filtriranje():any {
    //
    // if(this.filter_ime_prezime && (!this.filter_opstina ||  this.opstina==''))
    //   return this.studentPodaci.filter((x:StudentGetAllResponse)=>{
    //     (x.ime+' '+x.prezime).toLowerCase().startsWith(this.ime_prezime.toLowerCase()) ||
    //     (x.prezime+' '+x.ime).toLowerCase().startsWith(this.ime_prezime.toLowerCase())
    //   })
    // else if(this.filter_opstina && (!this.filter_ime_prezime || this.ime_prezime==''))
    //   return this.studentPodaci.filter((x:StudentGetAllResponse)=>{
    //     x.opstina_rodjenja.description.toLowerCase().startsWith(this.opstina.toLowerCase())
    //   })
    // else if((this.filter_opstina && this.filter_ime_prezime) && this.ime_prezime!='' && this.opstina!='')
    //   return this.studentPodaci.filter((x:StudentGetAllResponse)=>{
    //     ((x.ime+' '+x.prezime).toLowerCase().startsWith(this.ime_prezime.toLowerCase()) ||
    //     (x.prezime+' '+x.ime).toLowerCase().startsWith(this.ime_prezime.toLowerCase())) &&
    //     (  x.opstina_rodjenja.description.toLowerCase().startsWith(this.opstina.toLowerCase()))
    //   })
    // else if(!this.filter_ime_prezime && !this.opstina)
    //   return this.studentPodaci;
    //return this.studentPodaci;
    if(this.filter_ime_prezime && (!this.filter_opstina || this.opstina==''))
      return this.studentPodaci.filter(x=> (x.ime +" "+x.prezime).toLowerCase().startsWith(this.ime_prezime.toLowerCase())
        || (x.prezime +" "+x.ime).toLowerCase().startsWith(this.ime_prezime.toLowerCase()))
    else if(this.filter_opstina && (!this.filter_ime_prezime||this.ime_prezime==''))
      return this.studentPodaci.filter(x => (x.opstina_rodjenja).description.toLowerCase().startsWith(this.opstina.toLowerCase()))
    else if((this.filter_opstina && this.filter_ime_prezime) && this.opstina!='' && this.ime_prezime!='')
      return this.studentPodaci.filter(x=>((x.ime +" "+x.prezime).toLowerCase().startsWith(this.ime_prezime.toLowerCase())
        || (x.prezime +" "+x.ime).toLowerCase().startsWith(this.ime_prezime.toLowerCase())) &&(x.opstina_rodjenja).description.toLowerCase().startsWith(this.opstina.toLowerCase()))
    else if(!this.filter_opstina && !this.filter_ime_prezime)
      return this.studentPodaci;
  }
  ngOnInit(): void {
    this.testirajWebApi();
    this.ucitajDefaultneOpstine();
    this.fetchOpstine()
    console.log(AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickiNalog)
    console.log(AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickiNalog.defaultOpstinaId)
  }

  FilterImePrezime() {
    this.filter_ime_prezime=!this.filter_ime_prezime
    if(!this.filter_ime_prezime)
      this.ime_prezime='';
  }

  FilterOpstina() {
    this.filter_opstina=!this.filter_opstina
    if(!this.filter_opstina)
      this.opstina='';
  }

  NoviStudent(){
    this.novi_student={
      id:this.id,
      ime:this.ime_prezime.charAt(0).toUpperCase()+this.ime_prezime.slice(1),
      prezime:'',
      opstina_rodjenja_id:AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickiNalog.defaultOpstinaId
    }
  }
  DodajNovogStudenta()
  {
    this.httpKlijent.post(MojConfig.adresa_servera+'/NoviStudent', this.novi_student, MojConfig.http_opcije()).subscribe(()=>{
      this.novi_student=null;
      porukaSuccess("Uspjesno ste dodali novog studenta!");
      this.testirajWebApi()
    })
  }

  EditujStudenta()
  {
    this.httpKlijent.post(MojConfig.adresa_servera+'/NoviStudent', this.edit_student, MojConfig.http_opcije()).subscribe(()=>{
      this.edit_student=null;
      porukaSuccess("Uspjesno ste editovali studenta!");
      this.testirajWebApi()
    })
  }
  fetchOpstine()
  {
    this.httpKlijent.get(MojConfig.adresa_servera+'/GetAllOpstine', MojConfig.http_opcije()).subscribe(x=>{
      this.opstine = x;
    })
  }

  editStudent(s:any) {
    this.edit_student={
      id:s.id,
      ime:s.ime,
      prezime:s.prezime,
      opstina_rodjenja_id:s.opstina_rodjenja_id
    }
  }

  SoftDelete(id:number) {
    this.httpKlijent.put(MojConfig.adresa_servera+'/SoftDelete', id, MojConfig.http_opcije()).subscribe(()=>{
      porukaSuccess("Uspjesno izbrisan student!");
      this.testirajWebApi()
    })
  }
}
