import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";
import {StudentGetAllResponse} from "../studenti/StudentGetAllResponse";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-student-maticnaknjiga',
  templateUrl: './student-maticnaknjiga.component.html',
  styleUrls: ['./student-maticnaknjiga.component.css']
})
export class StudentMaticnaknjigaComponent implements OnInit {
  showUpis: boolean=false;
  studentid:number
  student_ime:string
  student_prezime:string
  upisane:any
  datumUpisa: any;
  cijenaSkolarine:any
  godinaStudija:any
  akademskaGodinaId:any
  obnova:any
  akademskeGodine:any
  Id:any
  datumOvjere:Date=new Date()
  napomena:string
  showOvjera:boolean=false
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {
    this.route.params.subscribe(x=>{
      this.studentid=<number>x['id']
    })
  }

  GetUpisaneGod()
  {
    this.httpKlijent.get(MojConfig.adresa_servera+`/GetUpisaneById?id=${this.studentid}`, MojConfig.http_opcije()).subscribe(x=>{
      this.upisane=x;
      console.log(x);
    })
  }

  GetAkGodine()
  {
    this.httpKlijent.get(MojConfig.adresa_servera+'/AkademskeGodine/GetAll_ForCmb', MojConfig.http_opcije()).subscribe(
      x=>{
        this.akademskeGodine=x
        console.log(x)
      }
    )
  }

  UpisiGodinu()
  {
    let godina={
      studentId : this.studentid,
      datumUpisa:this.datumUpisa,
      cijenaSkolarine:this.cijenaSkolarine,
      akademskaGodinaId:this.akademskaGodinaId,
      godinaStudija:this.godinaStudija,
      obnova:this.obnova,
      evidentiraoId:AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickiNalog.id
    }
    this.httpKlijent.post(MojConfig.adresa_servera+'/UpisGodine',godina, MojConfig.http_opcije()).subscribe(x=>{
      porukaSuccess("Uspjesno ste upisali novi semestar!")
      this.showUpis=false
      this.GetUpisaneGod()

    })
  }
 UcitajPodatke(){
   this.httpKlijent.get<StudentGetAllResponse>(MojConfig.adresa_servera+`/GetStudentById?id=${this.studentid}`, MojConfig.http_opcije())
     .subscribe(x=>{
       this.student_ime=x.ime
       this.student_prezime=x.prezime
     })
 }

 OvjeriSemestar(){
    let o ={
      Id:this.Id,
      napomena:this.napomena,
      datumOvjere:this.datumOvjere
    }

    this.httpKlijent.post(MojConfig.adresa_servera+'/OvjeriSemestar',o , MojConfig.http_opcije()).subscribe(y=>{
      this.showOvjera=false;
      porukaSuccess("Uspjesno ste ovjerili semestar! :D")
      this.GetUpisaneGod()
    })
 }
  ngOnInit(): void {
   this.UcitajPodatke()
    this.GetUpisaneGod()
    this.GetAkGodine()
  }
}
