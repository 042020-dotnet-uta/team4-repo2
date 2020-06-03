export interface FormTemplate {
    id: string;
    type: "text" | "table";

    title: string;

    offsetTop: number;
    offsetLeft: number;
    xPos: number;
    yPos: number;

    width: number;
    height: number;
    
    labels: string[];
}