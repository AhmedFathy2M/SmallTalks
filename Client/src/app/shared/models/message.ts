
export interface IMessage {
  from: string | null;
  to?:string;
  content: string;
  time: Date;
  seen?:boolean;
  audio?:HTMLAudioElement;
  image?:HTMLImageElement;
}