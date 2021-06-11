/*!
 * froala_editor v3.2.6 (https://www.froala.com/wysiwyg-editor)
 * License https://froala.com/wysiwyg-editor/terms/
 * Copyright 2014-2021 Froala Labs
 */

!function(a,t){"object"==typeof exports&&"undefined"!=typeof module?t(require("froala-editor")):"function"==typeof define&&define.amd?define(["froala-editor"],t):t(a.FroalaEditor)}(this,function(a){"use strict";a=a&&a.hasOwnProperty("default")?a["default"]:a,Object.assign(a.DEFAULTS,{paragraphFormat:{N:"Normal",H1:"Heading 1",H2:"Heading 2",H3:"Heading 3",H4:"Heading 4",PRE:"Code"},paragraphFormatSelection:!1,paragraphDefaultSelection:"Paragraph Format"}),a.PLUGINS.paragraphFormat=function(h){var f=h.$;function g(a,t){var e=h.html.defaultTag();if(t&&t.toLowerCase()!=e)if(0<a.find("ul, ol").length){var r=f("<"+t+">");a.prepend(r);for(var n=h.node.contents(a.get(0))[0];n&&["UL","OL"].indexOf(n.tagName)<0;){var o=n.nextSibling;r.append(n),n=o}}else a.html("<"+t+">"+a.html()+"</"+t+">")}return{apply:function c(a){"N"==a&&(a=h.html.defaultTag()),h.selection.save(),h.html.wrap(!0,!0,!h.opts.paragraphFormat.BLOCKQUOTE,!0,!0),h.selection.restore();var t,e,r,n,o,i,p,l,s=h.selection.blocks();h.selection.save(),h.$el.find("pre").attr("skip",!0);for(var m=0;m<s.length;m++)if(s[m].tagName!=a&&!h.node.isList(s[m])){var d=f(s[m]);"LI"==s[m].tagName?g(d,a):"LI"==s[m].parentNode.tagName&&s[m]?(i=d,p=a,l=h.html.defaultTag(),p&&p.toLowerCase()!=l||(p='div class="fr-temp-div"'),i.replaceWith(f("<"+p+">").html(i.html()))):0<=["TD","TH"].indexOf(s[m].parentNode.tagName)?(r=d,n=a,o=h.html.defaultTag(),n||(n='div class="fr-temp-div"'+(h.node.isEmpty(r.get(0),!0)?' data-empty="true"':"")),n.toLowerCase()==o?(h.node.isEmpty(r.get(0),!0)||r.append("<br/>"),r.replaceWith(r.html())):r.replaceWith(f("<"+n+">").html(r.html()))):(t=d,(e=a)||(e='div class="fr-temp-div"'+(h.node.isEmpty(t.get(0),!0)?' data-empty="true"':"")),"H1"!=e&&"H2"!=e&&"H3"!=e&&"H4"!=e&&"H5"!=e||!h.node.attributes(t.get(0)).includes("font-size:")?t.replaceWith(f("<"+e+" "+h.node.attributes(t.get(0))+">").html(t.html()).removeAttr("data-empty")):t.replaceWith(f("<"+e+" "+h.node.attributes(t.get(0)).replace(/font-size:[0-9]+px;?/,"")+">").html(t.html()).removeAttr("data-empty")))}h.$el.find('pre:not([skip="true"]) + pre:not([skip="true"])').each(function(){f(this).prev().append("<br>"+f(this).html()),f(this).remove()}),h.$el.find("pre").removeAttr("skip"),h.html.unwrap(),h.selection.restore()},refreshOnShow:function i(a,t){var e=h.selection.blocks();if(e.length){var r=e[0],n="N",o=h.html.defaultTag();r.tagName.toLowerCase()!=o&&r!=h.el&&(n=r.tagName),t.find('.fr-command[data-param1="'+n+'"]').addClass("fr-active").attr("aria-selected",!0)}else t.find('.fr-command[data-param1="N"]').addClass("fr-active").attr("aria-selected",!0)},refresh:function o(a){if(h.opts.paragraphFormatSelection){var t=h.selection.blocks();if(t.length){var e=t[0],r="N",n=h.html.defaultTag();e.tagName.toLowerCase()!=n&&e!=h.el&&(r=e.tagName),0<=["LI","TD","TH"].indexOf(r)&&(r="N"),a.find(">span").text(h.language.translate(h.opts.paragraphFormat[r]))}else a.find(">span").text(h.language.translate(h.opts.paragraphFormat.N))}}}},a.RegisterCommand("paragraphFormat",{type:"dropdown",displaySelection:function(a){return a.opts.paragraphFormatSelection},defaultSelection:function(a){return a.language.translate(a.opts.paragraphDefaultSelection)},displaySelectionWidth:80,html:function(){var a='<ul class="fr-dropdown-list" role="presentation">',t=this.opts.paragraphFormat;for(var e in t)if(t.hasOwnProperty(e)){var r=this.shortcuts.get("paragraphFormat."+e);r=r?'<span class="fr-shortcut">'+r+"</span>":"",a+='<li role="presentation"><'+("N"==e?this.html.defaultTag()||"DIV":e)+' style="padding: 0 !important; margin: 0 !important; border: 0 !important; background-color: transparent !important; '+("PRE"==e||"N"==e?"font-size: 15px":"font-weight: bold !important; ")+("H1"==e?"font-size: 2em !important; ":"")+("H2"==e?"font-size: 1.5em !important; ":"")+("H3"==e?"font-size: 1.17em !important; ":"")+("H4"==e?"font-size: 15px !important;":"")+'"  role="presentation"><a class="fr-command" tabIndex="-1" role="option" data-cmd="paragraphFormat" data-param1="'+e+'" title="'+this.language.translate(t[e])+'">'+this.language.translate(t[e])+"</a></"+("N"==e?this.html.defaultTag()||"DIV":e)+"></li>"}return a+="</ul>"},title:"Paragraph Format",callback:function(a,t){this.paragraphFormat.apply(t)},refresh:function(a){this.paragraphFormat.refresh(a)},refreshOnShow:function(a,t){this.paragraphFormat.refreshOnShow(a,t)},plugin:"paragraphFormat"}),a.DefineIcon("paragraphFormat",{NAME:"paragraph",SVG_KEY:"paragraphFormat"})});