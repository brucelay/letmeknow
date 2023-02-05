import WorkflowBuilder from "./components/WorkflowBuilder";

export default function App() {
  return (
    <>
      <div class="flex flex-col h-full">
        <div class="basis-2/12 p-5 font-bold shadow" >
          <h1 class="text-7xl table-header-group">LetMe<span class="text-lime-600">Know</span></h1>
        </div>

        <WorkflowBuilder />
      </div>
    </>
  );
}
