import { Component } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  email;
  fileRequestContent;
  encodedText;
  encodingKey;
  decodedText;
  show = false;
  loadingUpload = false;
  loadingDecode = false;

  constructor(private httpService: HttpService) {
  }

  uploadFile(): void {
    this.loadingUpload = true;
    this.httpService.uploadContent({ email: this.email, content: this.fileRequestContent })
      .subscribe(_ => {
        this.show = true;
        this.loadingUpload = false;
        setTimeout(() => {
          this.show = false;
        }, 3000);
      }, _ => this.loadingUpload = false);
  }

  decodeText(): void {
    this.loadingDecode = true;
    this.httpService.decodeContent({ content: this.encodedText, encodingKey: this.encodingKey })
      .subscribe(decodedText => {
        this.loadingDecode = false;
        this.decodedText = decodedText;
      }, _ => this.loadingDecode = false);
  }
}
