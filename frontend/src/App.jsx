import WorkflowBuilder from "./components/WorkflowBuilder";

export default function App() {
  return (
    <>
      <div class="flex flex-col h-full">
        <div class="basis-2/12">
          <h1 class="text-5xl">LetMeKnow</h1>
        </div>
        <WorkflowBuilder />
      </div>
    </>
  );
}
