import WorkflowBuilder from "./components/WorkflowBuilder";

export default function App() {
  return (
    <>
      <div class="flex flex-col h-full">
        <div class="basis-2/12 p-5 ml-2 mt-2 font-bold shadow" >
          <h1 id="title" class="text-7xl table-header-group">LetMe<span class="text-orange-600">Know</span>,</h1>
        </div>

        <WorkflowBuilder />
      </div>
    </>
  );
}
