import { Command } from "src/app/shared/models/command.model";
import { AppComponent } from "src/app/app.component";

export class OpenModalCommand implements Command {

  data: any;

  constructor(private appComponent: AppComponent) { }

  execute(): void {
    this.appComponent.isModalVisible = true;
    this.appComponent.data = this.data;
  }
}
