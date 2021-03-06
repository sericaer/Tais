﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais.Run
{
    interface Integration
    {
        object srcObj { get; }
        object destObj { get; }

        
        List<(LambdaExpression src, LambdaExpression dest)> binds { get; }
    }

    class IntegrationImp<TS, TD> : Integration where TS : class, INotifyPropertyChanged
    {
        public object srcObj { get; }
        public object destObj { get; }


        public List<(LambdaExpression src, LambdaExpression dest)> binds { get; }

        public IntegrationImp(TS srcObj, TD destObj)
        {
            this.srcObj = srcObj;
            this.destObj = destObj;

            binds = new List<(LambdaExpression src, LambdaExpression dest)>();
        }

        public IntegrationImp(TS srcObj, IEnumerable<TD> destObj)
        {
            this.srcObj = srcObj;
            this.destObj = destObj;

            binds = new List<(LambdaExpression src, LambdaExpression dest)>();
        }

        public IntegrationImp(IEnumerable<TS> srcObj, TD destObj)
        {
            this.srcObj = srcObj;
            this.destObj = destObj;

            binds = new List<(LambdaExpression src, LambdaExpression dest)>();
        }

        public void With<TP>(Expression<Func<TS, TP>> src, Expression<Func<TD, Action<TP>>> dest, bool isIgnoreInit = false)
        {
            binds.Add((src, dest));

            int skip = isIgnoreInit ? 1 : 0;


            if(destObj is IEnumerable<TD>)
            {
                foreach(var elem in (destObj as IEnumerable<TD>))
                {
                    ((TS)srcObj).OBSProperty(src).Skip(skip).Subscribe(dest.Compile().Invoke((TD)elem));
                }
                return;
            }

            if(srcObj is IEnumerable<TS>)
            {
                foreach (var elem in (srcObj as IEnumerable<TS>))
                {
                    ((TS)elem).OBSProperty(src).Skip(skip).Subscribe(dest.Compile().Invoke((TD)destObj));
                }
                return;
            }

            ((TS)srcObj).OBSProperty(src).Skip(skip).Subscribe(dest.Compile().Invoke((TD)destObj));
        }
    }
}
