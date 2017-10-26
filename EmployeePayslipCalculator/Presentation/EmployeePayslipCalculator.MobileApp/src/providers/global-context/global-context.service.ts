import { Injectable } from '@angular/core';
import { ToastController, LoadingController, AlertController, Loading, Alert, Toast } from 'ionic-angular';

@Injectable()
export class GlobalContextService {
  /**API url */
  // public server = 'http://localhost:58258/api/';//
  public server = 'http://employeepayslipcalculatorwebapi.azurewebsites.net/api/';



  public loader: Loading;
  
  public alert: Alert;
  constructor(private toastCtrl: ToastController, public loadingCtrl: LoadingController, public alertCtrl: AlertController) {
    console.log('Hello UiServiceProvider Provider');
  }

  showToast(message: string) {
    let toast = this.toastCtrl.create({
      message: message,
      duration: 2500,
      position: 'middle'
    });

    // this.toast.onDidDismiss(() => {
    //   console.log('Dismissed toast');
    // });

    toast.present();
  }

  showAlert(message: string) {
    let alert = this.alertCtrl.create({
      title: 'Alert!',
      subTitle: message,
    });

    alert.present();
  }

  showLoading(content = "Loading...") {
    this.loader = this.loadingCtrl.create({
      content: content,
      duration: 30000
    });
    this.loader.present();
  }

  hideLoading() {
    this.loader.dismissAll();
  }


}
