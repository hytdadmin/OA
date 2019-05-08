/* --navigation scripts START-- */
function foldNav(obj) {
	if (is_ignorable( obj ) )
		obj = obj.parentNode
	if (obj.className.substr(obj.className.length - 1, 1) == "R") {
		obj.className = obj.className.substr(0, obj.className.length-1);
		try {
			if (node_after(obj.parentNode).id.indexOf("_container") > -1){
				node_after(obj.parentNode).style.display = "none";
			}
		} catch (ex) {
			// nothing
		}
} else {
            containeClearR();
		obj.className += "R";
		try {
		    if (node_after(obj.parentNode).id.indexOf("_container") > -1) {
                containerNone();
				//node_after(obj.parentNode).style.display = "block";
                document.getElementById(node_after(obj.parentNode).id).style.display="block";
			}
		} catch (ex) {
			// nothing
		}
	}
}

//隐藏二级导航
function containerNone() {
    var _ches = document.getElementsByName("container");
    for (var i = 0; i < _ches.length; i++) {
        //$("#"+_ches[i].id).css("display","none");
        document.getElementById(_ches[i].id).style.display = "none";
    }
}

//一级导航的classname去掉“R”
function containeClearR(){
                var _ches = document.getElementsByName("navMain");
                for (var i = 0; i < _ches.length; i++) {
                    //$("#"+_ches[i].id).css("display","none");
	if ( _ches[i].className.substr(_ches[i].className.length-1, 1) == "R" ) {
		_ches[i].className = _ches[i].className.substr(0, _ches[i].className.length-1);
                }
                }
}
function setNav(navID){
	refLayer = document.getElementById(navID);
	if (refLayer.firstChild.className.substr(refLayer.firstChild.className.length-1, 1) != "R"){
		refLayer.firstChild.className += "R";
		refLayer.firstChild.style.display = "block";
	}
	if ( refLayer.parentNode.id.indexOf("_container") > -1 ){
		refLayer.parentNode.style.display = "block";
		var prevSib = node_before(refLayer.parentNode);
		if (prevSib.firstChild.className.substr(prevSib.firstChild.className.length-1, 1) != "R"){
			prevSib.firstChild.className += "R";
		}
		foldNav(refLayer.parentNode.firstChild, 1);
	}
	try{
		if (node_after(refLayer).id.indexOf("_container") > -1 && node_after(refLayer).style.display == "none") {
			node_after(refLayer).style.display = "block"
		}
	} catch (ex){
		// nothing
	}
	if (refLayer.parentNode.parentNode.id != "dvContent"){
		setNav(node_before(refLayer.parentNode).id);
	}
}

function toggleHref(n) {
	for(i=1; i<30; i++ )	{
	    if (i == n) {
	        document.getElementById("link" + i).className = "nolink";
	    }
	    else {
	        if ($("#link" + i).length > 0) {
	            document.getElementById("link" + i).className = "navSubEmpty";
	        }
	    }
	}		
}
/* --navigation scripts END-- */

/* --iframe Height START
function handleOnLoad() {
 var hDoc = getBodyHeight(document);
 var tblmain =  document.getElementById('tblMain');
 var mFrame = window.mainFrame;
 var hFrameDoc = getFrameHeight(mFrame);
 var hTable = hDoc-84;
 if(hFrameDoc > hTable) hTable = hFrameDoc;
 tblmain.style.height = hTable;
 if(window.mainFrame.bFrame != null){
	initFrameHeight(window.mainFrame,hTable);
 }
}
function initFrameHeight(frame,hFrame){
  var tblContent = frame.document.getElementById('tblContent');
  tblContent.style.height = hFrame;
}
function getBodyHeight(doc){
  if(doc.all) return doc.body.offsetHeight;
  else return doc.body.scrollHeight;
}
function getFrameHeight(mFrame){
  var h1 = mFrame.document.body.offsetHeight;
  var h2 = mFrame.document.body.scrollHeight;
  if(mFrame.bFrame != null){
  	var h3 = mFrame.bFrame.document.body.scrollHeight;
	initFrameHeight(mFrame,h3);
	h2 = mFrame.document.body.scrollHeight;
	if(h3 > h2) h2 = h3;
	alert(h2+','+h3);
  }
  return h2;
}
window.onload = handleOnLoad;
iframe Height END-- */

/* --tel scolling START-- */
self.onError=null;
currentX = currentY = 0;  
whichIt = null;           
lastScrollX = 0; lastScrollY = 0;
NS = (document.layers) ? 1 : 0;
IE = (document.all) ? 1: 0;
//<!-- STALKER CODE -->
function heartBeat() {
//	if(IE) { diffY = document.body.scrollTop; diffX = document.body.scrollLeft; }
//	if(NS) { diffY = self.pageYOffset; diffX = self.pageXOffset; }
//	if(diffY != lastScrollY) {
//				percent = .1 * (diffY - lastScrollY);
//				if(percent > 0) percent = Math.ceil(percent);
//				else percent = Math.floor(percent);
//				if(IE) document.all.floater.style.pixelTop += percent;
//				if(NS) document.floater.top += percent; 
//				lastScrollY = lastScrollY + percent;
//	}
//	if(diffX != lastScrollX) {
//		percent = .1 * (diffX - lastScrollX);
//		if(percent > 0) percent = Math.ceil(percent);
//		else percent = Math.floor(percent);
//		if(IE) document.all.floater.style.pixelLeft += percent;
//		if(NS) document.floater.left += percent;
//		lastScrollX = lastScrollX + percent;
//	}	
}
/*<!-- /STALKER CODE -->
<!-- DRAG DROP CODE -->*/
function checkFocus(x,y) { 
		stalkerx = document.floater.pageX;
		stalkery = document.floater.pageY;
		stalkerwidth = document.floater.clip.width;
		stalkerheight = document.floater.clip.height;
		if( (x > stalkerx && x < (stalkerx+stalkerwidth)) && (y > stalkery && y < (stalkery+stalkerheight))) return true;
		else return false;
}
function grabIt(e) {
//	if(IE) {
//		whichIt = event.srcElement;
//		while (whichIt.id.indexOf("floater") == -1) {
//			whichIt = whichIt.parentElement;
//			if (whichIt == null) { return true; }
//		}
//		whichIt.style.pixelLeft = whichIt.offsetLeft;
//		whichIt.style.pixelTop = whichIt.offsetTop;
//		currentX = (event.clientX + document.body.scrollLeft);
//		currentY = (event.clientY + document.body.scrollTop); 	
//	} else { 
//		window.captureEvents(Event.MOUSEMOVE);
//		if(checkFocus (e.pageX,e.pageY)) { 
//				whichIt = document.floater;
//				StalkerTouchedX = e.pageX-document.floater.pageX;
//				StalkerTouchedY = e.pageY-document.floater.pageY;
//		} 
//	}
	return true;
}
function moveIt(e) {
	if (whichIt == null) { return false; }
	if(IE) {
		newX = (event.clientX + document.body.scrollLeft);
		newY = (event.clientY + document.body.scrollTop);
		distanceX = (newX - currentX);    distanceY = (newY - currentY);
		currentX = newX;    currentY = newY;
		whichIt.style.pixelLeft += distanceX;
		whichIt.style.pixelTop += distanceY;
		if(whichIt.style.pixelTop < document.body.scrollTop) whichIt.style.pixelTop = document.body.scrollTop;
		if(whichIt.style.pixelLeft < document.body.scrollLeft) whichIt.style.pixelLeft = document.body.scrollLeft;
		if(whichIt.style.pixelLeft > document.body.offsetWidth - document.body.scrollLeft - whichIt.style.pixelWidth - 20) whichIt.style.pixelLeft = document.body.offsetWidth - whichIt.style.pixelWidth - 20;
		if(whichIt.style.pixelTop > document.body.offsetHeight + document.body.scrollTop - whichIt.style.pixelHeight - 5) whichIt.style.pixelTop = document.body.offsetHeight + document.body.scrollTop - whichIt.style.pixelHeight - 5;
		event.returnValue = false;
	} else { 
		whichIt.moveTo(e.pageX-StalkerTouchedX,e.pageY-StalkerTouchedY);
		if(whichIt.left < 0+self.pageXOffset) whichIt.left = 0+self.pageXOffset;
		if(whichIt.top < 0+self.pageYOffset) whichIt.top = 0+self.pageYOffset;
		if( (whichIt.left + whichIt.clip.width) >= (window.innerWidth+self.pageXOffset-17)) whichIt.left = ((window.innerWidth+self.pageXOffset)-whichIt.clip.width)-17;
		if( (whichIt.top + whichIt.clip.height) >= (window.innerHeight+self.pageYOffset-17)) whichIt.top = ((window.innerHeight+self.pageYOffset)-whichIt.clip.height)-17;
		return false;
	}
	return false;
}
function dropIt() {
	whichIt = null;
	if(NS) window.releaseEvents (Event.MOUSEMOVE);
	return true;
}
/*<!-- DRAG DROP CODE -->*/
if(NS) {
	window.captureEvents(Event.MOUSEUP|Event.MOUSEDOWN);
	window.onmousedown = grabIt;
	window.onmousemove = moveIt;
	window.onmouseup = dropIt;
}
if(IE) {
	document.onmousedown = grabIt;
	document.onmousemove = moveIt;
	document.onmouseup = dropIt;
}
if(NS || IE) action = window.setInterval("heartBeat()",1);
/* --tel scolling START-- */

//全选
function selectall(obj) {
    var c = obj.checked;
    var s = $(".che1 input");
    for (var i = 0; i < s.length; i++) {
        s[i].checked = c;
    }
}