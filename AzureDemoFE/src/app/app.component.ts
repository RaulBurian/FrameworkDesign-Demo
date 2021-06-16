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

  constructor(private httpService: HttpService) {
  }

  uploadFile(): void {
    this.httpService.uploadContent({ email: this.email, content: this.fileRequestContent })
      .subscribe(_ => {
        this.show = true;
        setTimeout(() => {
          this.show = false;
        }, 3000);
      });
  }

  decodeText(): void {
    this.httpService.decodeContent({ content: this.encodedText, encodingKey: this.encodingKey })
      .subscribe(decodedText => this.decodedText = decodedText);
  }
}
