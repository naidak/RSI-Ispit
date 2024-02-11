export interface StudentGetAllResponse{
  id: number,
  korisnickoIme: string,
  slika_korisnika: string,
  isNastavnik: boolean,
  isStudent: boolean,
  isAdmin: boolean,
  isProdekan: boolean,
  isDekan: boolean,
  isStudentskaSluzba: boolean,
  defaultOpstinaId: 0,
  defaultOpstina: Opstina,
  ime: string,
  prezime: string,
  broj_indeksa: string,
  opstina_rodjenja_id: 0,
  opstina_rodjenja: Opstina,
  created_time: string
}

export interface Opstina{
  id: number
  description: string,
  drzava_id: 0,
  drzava: Drzava
  }

  export interface Drzava{
    id: number,
    naziv: string
}
