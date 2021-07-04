import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class DocumentService {
  constructor(private http: HttpClient) { }
  GetDocuments(): Observable<any>{
    return this.http.get(environment.apiUrl+'/document/docslist',{headers: {'Accept': 'application/json',
    'Authorization' : 'Bearer ' +
    localStorage.getItem('Bearer')}})
    
  }
  DownloadDocument(id: number): Observable<any>{
    return this.http.get(environment.apiUrl+'/document/download?docId='+id,{headers: {'Accept': 'application/json',
    'Authorization' : 'Bearer ' +
    localStorage.getItem('Bearer')}, responseType: 'blob' } )
  }
  AddDocument(file: any): Observable<any>{
    const formData = new FormData();
    formData.append('file', file, file.name);
    return this.http.post('https://localhost:34502/api/document/', formData)
  } 
}
