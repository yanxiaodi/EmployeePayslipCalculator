import { PayslipInfo } from './../../models/payslipInfo';
import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';

/**
 * Generated class for the ShowResultPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@Component({
  selector: 'page-show-result',
  templateUrl: 'show-result.html',
})
export class ShowResultPage {
  public payslipInfo: PayslipInfo;
  constructor(public navCtrl: NavController, public navParams: NavParams) {
    this.payslipInfo = navParams.data;
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad ShowResultPage');
  }

}
