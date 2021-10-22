import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DocumentService } from 'src/services/document.service';
@Component({
  selector: 'app-docs-storage',
  templateUrl: './docs-storage.component.html',
  styleUrls: ['./docs-storage.component.scss']
})
export class DocsStorageComponent implements OnInit {

  constructor(private router: Router, public _docService: DocumentService) { }

  docs: any[] = []
  file: File | undefined
  ngOnInit(): void {
    
    this._docService.GetDocuments().subscribe(data=>{
      this.docs=data.items
    })
  }

  Download(id: number){
    this._docService.DownloadDocument(id).subscribe(blob=>{
      var downloadURL = window.URL.createObjectURL(blob);
  var link = document.createElement('a');
  link.href = downloadURL;
  link.download = this.docs.find(c=>c.id==id).fileName;
  link.click();
  })

}
}
