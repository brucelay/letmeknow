import { createEffect, createSignal } from "solid-js";

export default function SummariseModule(props) {
  const [maxTokens, setMaxTokens] = createSignal("200");

  createEffect( () => {
    props.setFunctions( (functions) => {
        functions[props.index()].options = {
            maxtokens: maxTokens()
        }
        return functions;
    })
  });

  return (
    <div class="bg-slate-50 p-3 flex flex-col">
      <div class="flex">
        <h2 class="text-xl mb-2">{props.index}: Summarise</h2>
      </div>
      <div class="flex rounded border-2 border-slate-200">
        <div class="p-2 px-3">
          <span>Tokens</span>
        </div>
        <input onInput={(e) => setMaxTokens(e.target.value)} type="text" class="block p-2 w-full" />
      </div>
    </div>
  );
}
