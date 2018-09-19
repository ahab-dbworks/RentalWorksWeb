﻿routes.push({ pattern: /^module\/markettype$/, action: function (match: RegExpExecArray) { return MarketTypeController.getModuleScreen(); } });  
 
class MarketType { 
  Module: string; 
  apiurl: string; 
 
  constructor() { 
    this.Module = 'MarketType'; 
    this.apiurl = 'api/v1/markettype'; 
  } 
 
  getModuleScreen() { 
    var screen, $browse; 
 
    screen = {}; 
    screen.$view = FwModule.getModuleControl(this.Module + 'Controller'); 
    screen.viewModel = {}; 
    screen.properties = {}; 
 
    $browse = this.openBrowse(); 
 
    screen.load = function () { 
      FwModule.openModuleTab($browse, 'Market Type', false, 'BROWSE', true); 
      FwBrowse.databind($browse); 
      FwBrowse.screenload($browse); 
    }; 
    screen.unload = function () { 
      FwBrowse.screenunload($browse); 
    }; 
 
    return screen; 
  } 
 
  openBrowse() { 
    var $browse; 
 
    $browse = FwBrowse.loadBrowseFromTemplate(this.Module); 
    $browse = FwModule.openBrowse($browse); 
 
    return $browse; 
  }
  
  openForm(mode: string) { 
    var $form; 
 
    $form = FwModule.loadFormFromTemplate(this.Module); 
    $form = FwModule.openForm($form, mode); 
 
    return $form; 
  } 
 
  loadForm(uniqueids: any) { 
    var $form; 
 
    $form = this.openForm('EDIT'); 
    $form.find('div.fwformfield[data-datafield="MarketTypeId"] input').val(uniqueids.MarketTypeId); 
    FwModule.loadForm(this.Module, $form); 
 
    return $form; 
  } 
 
  saveForm($form: any, parameters: any) { 
    FwModule.saveForm(this.Module, $form, parameters); 
  } 
 
  loadAudit($form: any) { 
    var uniqueid; 
    uniqueid = $form.find('div.fwformfield[data-datafield="MarketTypeId"] input').val(); 
    FwModule.loadAudit($form, uniqueid); 
  } 
 
  afterLoad($form: any) { 
 
  } 
} 
 
var MarketTypeController = new MarketType();