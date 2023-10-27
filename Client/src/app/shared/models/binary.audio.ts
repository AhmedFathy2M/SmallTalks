export interface IBinaryAudio {
    from: string | null;
    to?:string;
    content: Uint8Array;
    time: Date;
    seen?:boolean;
  }