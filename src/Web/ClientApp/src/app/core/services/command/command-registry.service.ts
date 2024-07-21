import { Injectable } from '@angular/core';
import { Command } from 'src/app/shared/models/command.model';

@Injectable({
  providedIn: 'root',
})
export class CommandRegistry {
  private commands: { [key: string]: Command } = {};

  registerCommand(commandName: string, command: Command): void {
    this.commands[commandName] = command;
  }

  executeCommand(commandName: string, data: any): void {
    const command = this.commands[commandName];

    if (data) {
      command.data = data;
    }

    if (command) {
      command.execute();
      return;
    }

    console.error(`Command ${commandName} not found or not registered`);
  }
}
