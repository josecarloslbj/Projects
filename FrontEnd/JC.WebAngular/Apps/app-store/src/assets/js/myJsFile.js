function beep(freq = 660, duration = 90, vol = 50) {
  var context = new (window.AudioContext || window.webkitAudioContext);
  const oscillator = context.createOscillator();
  const gain = context.createGain();
  gain.gain.setValueAtTime(0, context.currentTime);
  gain.gain.linearRampToValueAtTime(1, context.currentTime + 0.002);
  oscillator.connect(gain);
  oscillator.frequency.value = freq;
  oscillator.type = "square";
  gain.connect(context.destination);
  oscillator.start(context.currentTime);
  oscillator.stop(context.currentTime + duration * .001);
  oscillator.onended = () => context.close();
}

function beepDelete(freq = 1200, duration = 90, vol = 90) {
  var context = new (window.AudioContext || window.webkitAudioContext);
  const oscillator = context.createOscillator();
  const gain = context.createGain();
  gain.gain.setValueAtTime(0, context.currentTime);
  gain.gain.linearRampToValueAtTime(1, context.currentTime + 0.002);
  oscillator.connect(gain);
  oscillator.frequency.value = freq;
  oscillator.type = "square";
  gain.connect(context.destination);
  oscillator.start(context.currentTime);
  oscillator.stop(context.currentTime + duration * .001);
  oscillator.onended = () => context.close();
}

function onF1() {
  return false;
}

function onF5() {

  return false;
}

function onEsc() {
  return true;
}

window.onkeydown = evt => {
  switch (evt.keyCode) {
    //ESC
    case 27:
      onEsc();
      break;
    //F1
    case 112:
      onF1();
      break;
    case 116:
      onF5();
      break;
    //Fallback to default browser behaviour
    default:
      return true;
  }
  //Returning false overrides default browser event
  return false;
};

window.onhelp = function () {
  return false;
};

window.onload = function () {

};