import { Component, OnInit } from '@angular/core';
import { TaskService } from '../../core/services/task.service';
import { Task } from '../../models/task-model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [FormsModule, CommonModule, HttpClientModule],
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.css'
})

export class TaskListComponent implements OnInit{
  tasks: Task[] = [];
  selectedTask: Task | null = null;
  showTaskForm: boolean = false;
  newTask: Task = {
    id: '', // Replace with your method to generate a GUID
    name: '',               // This will come from your form input
    isComplete: undefined,       // Change 'completed' to 'isComplete'
  };

  constructor(private taskService: TaskService) {}
  
  ngOnInit(): void {
    this.taskService.getTasks().subscribe(
      (response: any) => {
        this.tasks = response.data;
      },
      (error) => {
        console.error('Error occurred while fetching task items:', error);
      }
    )
  }

  generateGuid(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
      const r = Math.random() * 16 | 0;
      const v = c === 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }

  resetForm(): void {
    this.newTask = {
      id: '', 
      name: '', 
      isComplete: false 
    };
  }

  getTasks(): void {
    this.taskService.getTasks().subscribe(
      (data) => { 
        this.tasks = data; 
      }
    )
  }

  createTask(): void {
    this.taskService.createTask(this.newTask).subscribe(
      (response) => {       
        this.tasks.push(response.data);
        this.showTaskForm = false;
        this.resetForm;
      },
      (error) => {
        console.error('Error adding task item:', error);
      }
    );
  }

  updateTask(task: Task): void {
    const updatedTask = this.tasks.find(t => t.id = task.id);

    if (!updatedTask) {
      console.error('Task not found');
      return;
    }
    updatedTask.isComplete = true;

    this.taskService.updateTask(updatedTask).subscribe(
      (response) => {
        console.log('Task marked complete successfully:', response);
      },
      (error) => {
        console.error('Error marking task complete:', error);
      }
    )
  }

  deleteTask(id: string): void {
    this.taskService.deleteTask(id).subscribe(
      () => {
        this.tasks = this.tasks.filter(t => t.id !== id);
      }
    )
  }

}
